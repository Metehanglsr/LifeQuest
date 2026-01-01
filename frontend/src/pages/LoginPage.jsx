import { useFormik } from 'formik';
import * as Yup from 'yup';
import authService from '../services/authService';
import { useAuth } from '../context/authContext';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const LoginPage = () => {
  const { login } = useAuth();
  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: { email: '', password: '' },
    validationSchema: Yup.object({
      email: Yup.string().email('Geçersiz email').required('Email zorunlu'),
      password: Yup.string().required('Şifre zorunlu'),
    }),
    onSubmit: async (values) => {
      try {
        const response = await authService.login(values);
        const token = response.token;
        if (token) {
          login(token);
          toast.success("Hoş geldin Şampiyon! 🚀");
          navigate('/');
        } else {
          toast.error("Giriş yapılamadı.");
        }
      } catch (error) {
        toast.error("Giriş başarısız! Bilgileri kontrol et.");
      }
    },
  });

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
      <div className="bg-white p-8 rounded-2xl shadow-xl w-full max-w-md border border-gray-100">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-indigo-600">LifeQuest 🚀</h1>
          <p className="text-gray-500 mt-2">Hayatını oyunlaştır, seviye atla!</p>
        </div>

        <form onSubmit={formik.handleSubmit} className="space-y-6">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Email Adresi</label>
            <input 
              type="email" 
              name="email"
              className="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition-all"
              placeholder="admin@lifequest.com"
              onChange={formik.handleChange}
              value={formik.values.email}
            />
            {formik.errors.email && <div className="text-red-500 text-xs mt-1">{formik.errors.email}</div>}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Şifre</label>
            <input 
              type="password" 
              name="password"
              className="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition-all"
              placeholder="••••••••"
              onChange={formik.handleChange}
              value={formik.values.password}
            />
            {formik.errors.password && <div className="text-red-500 text-xs mt-1">{formik.errors.password}</div>}
          </div>

          <button 
            type="submit" 
            className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-3 rounded-lg transition-colors shadow-lg hover:shadow-indigo-500/30"
          >
            Maceraya Başla
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;