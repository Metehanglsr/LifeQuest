import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import authService from "../services/authService";

const Register = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    name: "",
    surname: "",
    username: "",
    email: "",
    password: "",
    passwordConfirm: "",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (formData.password !== formData.passwordConfirm) {
      alert("Şifreler uyuşmuyor!");
      return;
    }

    setLoading(true);
    try {
      await authService.register({
        name: formData.name,
        surname: formData.surname,
        username: formData.username,
        email: formData.email,
        password: formData.password,
        passwordConfirm: formData.passwordConfirm,
      });

      alert("🎉 Kayıt başarılı! Giriş sayfasına yönlendiriliyorsun.");
      navigate("/login");
    } catch (err) {
      console.error(err);
      alert("Hata: " + (err.response?.data?.message || "Kayıt olunamadı."));
    } finally {
      setLoading(false);
    }
  };

  const inputClass =
    "w-full bg-slate-900/50 border border-slate-700 rounded-xl px-4 py-3 text-white outline-none focus:border-indigo-500 focus:bg-slate-900 transition-all placeholder:text-slate-600";

  return (
    <div className="min-h-screen bg-slate-950 flex items-center justify-center p-4 relative overflow-hidden">
      <div className="absolute top-0 left-0 w-96 h-96 bg-indigo-600/20 blur-[120px] rounded-full"></div>
      <div className="absolute bottom-0 right-0 w-96 h-96 bg-rose-600/10 blur-[120px] rounded-full"></div>

      <div className="w-full max-w-md bg-slate-900/40 backdrop-blur-xl border border-slate-800 p-8 rounded-[2rem] shadow-2xl relative z-10">
        <div className="text-center mb-8">
          <div className="w-16 h-16 bg-indigo-600 rounded-2xl mx-auto flex items-center justify-center text-3xl shadow-[0_0_30px_rgba(79,70,229,0.5)] mb-4">
            🚀
          </div>
          <h1 className="text-3xl font-black text-white italic tracking-tighter uppercase">
            LifeQuest
          </h1>
          <p className="text-slate-500 text-sm mt-2 font-medium">
            Maceraya Başlamak İçin Katıl
          </p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <input
              type="text"
              name="name"
              placeholder="Ad"
              required
              className={inputClass}
              onChange={handleChange}
            />
            <input
              type="text"
              name="surname"
              placeholder="Soyad"
              required
              className={inputClass}
              onChange={handleChange}
            />
          </div>

          <input
            type="text"
            name="username"
            placeholder="Kullanıcı Adı"
            required
            className={inputClass}
            onChange={handleChange}
          />

          <input
            type="email"
            name="email"
            placeholder="E-Posta Adresi"
            required
            className={inputClass}
            onChange={handleChange}
          />

          <input
            type="password"
            name="password"
            placeholder="Şifre"
            required
            className={inputClass}
            onChange={handleChange}
          />

          <input
            type="password"
            name="passwordConfirm"
            placeholder="Şifre Tekrar"
            required
            className={inputClass}
            onChange={handleChange}
          />

          <button
            disabled={loading}
            className="w-full bg-indigo-600 hover:bg-indigo-500 text-white font-black py-4 rounded-xl uppercase tracking-widest shadow-lg shadow-indigo-600/20 transition-all active:scale-[0.98] mt-4"
          >
            {loading ? "KAYDEDİLİYOR..." : "KAYIT OL"}
          </button>
        </form>

        <div className="mt-6 text-center">
          <p className="text-slate-500 text-sm">
            Zaten hesabın var mı?{" "}
            <Link
              to="/login"
              className="text-indigo-400 font-bold hover:text-indigo-300 underline decoration-2 underline-offset-4"
            >
              Giriş Yap
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Register;