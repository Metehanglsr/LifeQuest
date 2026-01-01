import { useEffect, useState } from 'react';
import taskService from '../services/taskService';
import userService from '../services/userService';
import { useAuth } from '../context/authContext';
import { getUserIdFromToken } from '../utils/authUtils';
import { toast } from 'react-toastify';

const Dashboard = () => {
  const [tasks, setTasks] = useState([]);
  const [leaderboard, setLeaderboard] = useState([]);
  const [userStats, setUserStats] = useState({ level: 1, xp: 0, name: 'Savaşçı' });
  
  const { logout } = useAuth();
  const userId = getUserIdFromToken();

  useEffect(() => {
    if (userId) fetchData();
  }, [userId]);

  const fetchData = async () => {
    try {
      const taskRes = await taskService.getAll(userId);
      setTasks(taskRes.data);

      try {
        const lbRes = await userService.getLeaderboard();
        setLeaderboard(lbRes.data);
      } catch (e) { console.log("Liderlik tablosu hatası"); }

      setUserStats({ level: 5, xp: 750, name: "Metehan" }); 
    } catch (err) {
      toast.error("Veriler yüklenemedi.");
    }
  };

  const handleComplete = async (taskId) => {
    try {
      const response = await taskService.complete(taskId);
      if(response.data.isSuccess) {
          toast.success(`Harika! +${response.data.earnedXP || 0} XP Kazandın! 🎉`);
          fetchData(); 
      }
    } catch (err) {
      toast.error("İşlem başarısız.");
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 font-sans text-gray-900">
      
      <nav className="bg-white shadow-sm border-b border-gray-200 sticky top-0 z-10">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between h-16 items-center">
            <div className="flex items-center gap-2">
              <span className="text-2xl">🚀</span>
              <h1 className="text-xl font-bold bg-gradient-to-r from-indigo-600 to-purple-600 bg-clip-text text-transparent">
                LifeQuest
              </h1>
            </div>
            <button 
              onClick={logout} 
              className="bg-red-50 text-red-600 hover:bg-red-100 px-4 py-2 rounded-lg text-sm font-medium transition-colors"
            >
              Çıkış Yap
            </button>
          </div>
        </div>
      </nav>

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div className="grid grid-cols-1 lg:grid-cols-4 gap-8">
          
          <div className="lg:col-span-1 space-y-6">
            <div className="bg-white rounded-2xl shadow-sm border border-gray-100 p-6 text-center">
              <div className="w-20 h-20 bg-indigo-100 text-indigo-600 rounded-full flex items-center justify-center text-3xl mx-auto mb-4 border-4 border-white shadow-lg">
                🦸‍♂️
              </div>
              <h2 className="text-xl font-bold text-gray-800">{userStats.name}</h2>
              <p className="text-indigo-500 font-medium text-sm">Level {userStats.level} Kahramanı</p>

              <div className="mt-6 text-left">
                <div className="flex justify-between text-xs font-semibold text-gray-500 mb-1">
                  <span>XP</span>
                  <span>{userStats.xp} / 1000</span>
                </div>
                <div className="w-full bg-gray-200 rounded-full h-2.5 overflow-hidden">
                  <div className="bg-gradient-to-r from-indigo-500 to-purple-500 h-2.5 rounded-full" style={{ width: '75%' }}></div>
                </div>
              </div>

              <div className="mt-6 pt-6 border-t border-gray-100">
                <h3 className="text-xs font-bold text-gray-400 uppercase tracking-wider mb-3">Rozetlerim</h3>
                <div className="flex justify-center gap-2">
                  <span className="p-2 bg-yellow-50 rounded-lg text-xl" title="Yeni Başlayan">🌱</span>
                  <span className="p-2 bg-blue-50 rounded-lg text-xl" title="Kitap Kurdu">📚</span>
                  <span className="p-2 bg-red-50 rounded-lg text-xl" title="Sporcu">💪</span>
                </div>
              </div>
            </div>
          </div>

          <div className="lg:col-span-2">
            <h2 className="text-xl font-bold text-gray-800 mb-4 flex items-center gap-2">
              <span>📜</span> Günlük Görevler
            </h2>
            
            {tasks.length === 0 ? (
              <div className="bg-white p-8 rounded-xl shadow-sm text-center text-gray-500">
                🎉 Harikasın! Tüm görevleri tamamladın.
              </div>
            ) : (
              <div className="space-y-4">
                {tasks.map((task) => (
                  <div key={task.id} className="bg-white p-5 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow group">
                    <div className="flex justify-between items-start">
                      <div>
                        <h3 className="font-bold text-gray-800 group-hover:text-indigo-600 transition-colors">
                          {task.title}
                        </h3>
                        <p className="text-sm text-gray-500 mt-1">{task.description}</p>
                        
                        <div className="flex gap-3 mt-3 text-xs font-semibold">
                          <span className={`px-2 py-1 rounded-md 
                            ${task.difficulty == 0 ? 'bg-green-100 text-green-700' : 
                              task.difficulty == 1 ? 'bg-yellow-100 text-yellow-700' : 'bg-red-100 text-red-700'}`}>
                            {task.difficulty == 0 ? 'Kolay' : task.difficulty == 1 ? 'Orta' : 'Zor'}
                          </span>
                          <span className="px-2 py-1 rounded-md bg-indigo-50 text-indigo-600">
                            ⚡ {task.baseXP} XP
                          </span>
                        </div>
                      </div>
                      
                      <button 
                        onClick={() => handleComplete(task.id)}
                        className="bg-gray-900 hover:bg-indigo-600 text-white p-3 rounded-lg transition-all transform active:scale-95 shadow-lg"
                        title="Görevi Tamamla"
                      >
                        ✅
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>

          <div className="lg:col-span-1">
            <div className="bg-white rounded-2xl shadow-sm border border-gray-100 overflow-hidden">
              <div className="bg-gradient-to-r from-yellow-400 to-orange-500 p-4 text-white">
                <h2 className="font-bold flex items-center gap-2">
                  <span>🏆</span> Lider Tablosu
                </h2>
              </div>
              <ul className="divide-y divide-gray-100">
                {leaderboard.length > 0 ? leaderboard.map((user, index) => (
                  <li key={index} className="p-4 flex justify-between items-center hover:bg-gray-50 transition-colors">
                    <div className="flex items-center gap-3">
                      <span className={`w-6 h-6 flex items-center justify-center rounded-full text-xs font-bold 
                        ${index === 0 ? 'bg-yellow-100 text-yellow-600' : 
                          index === 1 ? 'bg-gray-100 text-gray-600' : 
                          index === 2 ? 'bg-orange-100 text-orange-600' : 'text-gray-400'}`}>
                        {index + 1}
                      </span>
                      <span className="font-medium text-gray-700 text-sm">{user.userName}</span>
                    </div>
                    <span className="text-xs font-bold text-indigo-600 bg-indigo-50 px-2 py-1 rounded-full">
                      {user.totalXP} XP
                    </span>
                  </li>
                )) : (
                  <li className="p-4 text-center text-gray-400 text-sm">Veri yükleniyor...</li>
                )}
              </ul>
            </div>
            
            <div className="mt-6 bg-gradient-to-br from-indigo-600 to-purple-600 rounded-2xl p-6 text-white text-center shadow-lg">
              <div className="text-3xl mb-2">🔥</div>
              <h3 className="font-bold">Zinciri Kırma!</h3>
              <p className="text-indigo-100 text-sm mt-1">Bugün 3 görev daha yaparsan "İstikrar" rozetini kazanacaksın.</p>
            </div>
          </div>

        </div>
      </div>
    </div>
  );
};

export default Dashboard;