import React, { useState, useEffect } from "react";
import MainLayout from "../layouts/MainLayout";
import categoryService from "../services/categoryService";
import taskService from "../services/taskService";
import badgeService from "../services/badgeService";

const AdminPage = () => {
  const [activeTab, setActiveTab] = useState("tasks");
  const [categories, setCategories] = useState([]);
  const [jsonInput, setJsonInput] = useState("");
  const [loading, setLoading] = useState(false);

  const [taskData, setTaskData] = useState({
    title: "",
    description: "",
    categoryId: "",
    baseXP: 100,
    minLevel: 1,
    difficulty: 0,
  });
  const [catData, setCatData] = useState({
    name: "",
    description: "",
    iconPath: "🔹",
  });
  const [badgeData, setBadgeData] = useState({
    name: "",
    description: "",
    iconPath: "🏆",
    requiredLevel: 1,
    categoryId: "",
  });

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    try {
      const data = await categoryService.getAllCategories();
      const catList = Array.isArray(data) ? data : data?.categories || [];
      setCategories(catList);
    } catch (err) {
      console.error(err);
    }
  };

  const handleSingleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      if (activeTab === "tasks") await taskService.createTask(taskData);
      else if (activeTab === "categories")
        await categoryService.createCategory(catData);
      else if (activeTab === "badges") {
        if (!badgeData.categoryId) {
          alert("Lütfen rozet için bir kategori seçin!");
          setLoading(false);
          return;
        }
        await badgeService.createBadge(badgeData);
      }

      alert("✅ Başarıyla eklendi!");
      if (activeTab === "categories") loadCategories();
    } catch (err) {
      alert("Hata: " + (err.response?.data?.message || err.message));
    } finally {
      setLoading(false);
    }
  };

  const handleBulkSubmit = async () => {
    if (!jsonInput) return;
    setLoading(true);
    try {
      const parsed = JSON.parse(jsonInput);
      if (activeTab === "tasks") await taskService.createBulkTasks(parsed);
      else if (activeTab === "categories")
        await categoryService.createBulkCategories(parsed);
      else if (activeTab === "badges")
        await badgeService.createBulkBadges(parsed);

      alert("🚀 Toplu yükleme başarılı!");
      setJsonInput("");
      if (activeTab === "categories") loadCategories();
    } catch (err) {
      alert("JSON hatası veya API reddetti: " + err.message);
    } finally {
      setLoading(false);
    }
  };

  const inputClass =
    "w-full bg-slate-950 border border-slate-800 rounded-xl p-4 text-white outline-none focus:border-indigo-500 transition-colors placeholder:text-slate-600";
  const labelClass =
    "text-[10px] font-black text-slate-500 uppercase tracking-widest ml-1 mb-2 block";

  return (
    <MainLayout>
      <div className="max-w-6xl mx-auto space-y-10">
        <section>
          <h2 className="text-4xl font-black text-white italic tracking-tighter uppercase">
            ADMİN KONTROL PANELİ
          </h2>
          <div className="h-1.5 w-32 bg-rose-600 rounded-full shadow-[0_0_20px_rgba(225,29,72,0.4)] mt-2"></div>
        </section>

        <div className="flex gap-2 p-1 bg-slate-900 border border-slate-800 rounded-2xl w-fit">
          {["categories", "tasks", "badges"].map((tab) => (
            <button
              key={tab}
              onClick={() => setActiveTab(tab)}
              className={`px-6 py-3 rounded-xl font-black uppercase text-[10px] tracking-widest transition-all ${
                activeTab === tab
                  ? "bg-indigo-600 text-white shadow-lg"
                  : "text-slate-500 hover:text-slate-300 hover:bg-slate-800"
              }`}
            >
              {tab === "tasks"
                ? "⚔️ Görevler"
                : tab === "categories"
                ? "📂 Kategoriler"
                : "🏆 Rozetler"}
            </button>
          ))}
        </div>

        <div className="grid grid-cols-1 xl:grid-cols-2 gap-10">
          <div className="bg-slate-900/40 p-10 rounded-[3rem] border border-slate-800 relative overflow-hidden">
            <div className="absolute top-0 right-0 w-32 h-32 bg-indigo-500/10 blur-[60px] rounded-full"></div>
            <h3 className="text-white font-black italic mb-8 uppercase text-lg tracking-widest border-b border-slate-800 pb-4">
              Tekli{" "}
              {activeTab === "tasks"
                ? "Görev"
                : activeTab === "categories"
                ? "Kategori"
                : "Rozet"}{" "}
              Ekle
            </h3>

            <form
              onSubmit={handleSingleSubmit}
              className="space-y-6 relative z-10"
            >
              {activeTab === "tasks" && (
                <>
                  <div>
                    <label className={labelClass}>Görev Başlığı</label>
                    <input
                      type="text"
                      placeholder="Örn: 5KM Koşu"
                      className={inputClass}
                      onChange={(e) =>
                        setTaskData({ ...taskData, title: e.target.value })
                      }
                      required
                    />
                  </div>
                  <div>
                    <label className={labelClass}>Açıklama</label>
                    <textarea
                      rows="2"
                      placeholder="Detaylar..."
                      className={inputClass}
                      onChange={(e) =>
                        setTaskData({
                          ...taskData,
                          description: e.target.value,
                        })
                      }
                    />
                  </div>
                  <div>
                    <label className={labelClass}>Kategori</label>
                    <select
                      className={inputClass}
                      onChange={(e) =>
                        setTaskData({ ...taskData, categoryId: e.target.value })
                      }
                      required
                    >
                      <option value="">Seçiniz...</option>
                      {categories.map((c) => (
                        <option key={c.id} value={c.id}>
                          {c.name}
                        </option>
                      ))}
                    </select>
                  </div>
                  <div className="grid grid-cols-2 gap-4">
                    <div>
                      <label className={labelClass}>XP Ödülü</label>
                      <input
                        type="number"
                        placeholder="100"
                        className={inputClass}
                        onChange={(e) =>
                          setTaskData({
                            ...taskData,
                            baseXP: parseInt(e.target.value),
                          })
                        }
                      />
                    </div>
                    <div>
                      <label className={labelClass}>Min. Level</label>
                      <input
                        type="number"
                        placeholder="1"
                        className={inputClass}
                        onChange={(e) =>
                          setTaskData({
                            ...taskData,
                            minLevel: parseInt(e.target.value),
                          })
                        }
                      />
                    </div>
                  </div>
                  <div>
                    <label className={labelClass}>Zorluk Seviyesi</label>
                    <select
                      className={inputClass}
                      onChange={(e) =>
                        setTaskData({
                          ...taskData,
                          difficulty: parseInt(e.target.value),
                        })
                      }
                    >
                      <option value="0">Kolay (Easy)</option>
                      <option value="1">Orta (Medium)</option>
                      <option value="2">Zor (Hard)</option>
                    </select>
                  </div>
                </>
              )}

              {activeTab === "categories" && (
                <>
                  <div>
                    <label className={labelClass}>Kategori Adı</label>
                    <input
                      type="text"
                      placeholder="Örn: Yazılım"
                      className={inputClass}
                      onChange={(e) =>
                        setCatData({ ...catData, name: e.target.value })
                      }
                      required
                    />
                  </div>
                  <div>
                    <label className={labelClass}>Açıklama</label>
                    <input
                      type="text"
                      placeholder="Kategori açıklaması"
                      className={inputClass}
                      onChange={(e) =>
                        setCatData({ ...catData, description: e.target.value })
                      }
                    />
                  </div>
                  <div>
                    <label className={labelClass}>İkon (Emoji)</label>
                    <input
                      type="text"
                      placeholder="💻"
                      className={inputClass}
                      onChange={(e) =>
                        setCatData({ ...catData, iconPath: e.target.value })
                      }
                    />
                  </div>
                </>
              )}

              {activeTab === "badges" && (
                <>
                  <div>
                    <label className={labelClass}>Rozet Adı</label>
                    <input
                      type="text"
                      placeholder="Örn: Maratoncu"
                      className={inputClass}
                      onChange={(e) =>
                        setBadgeData({ ...badgeData, name: e.target.value })
                      }
                      required
                    />
                  </div>
                  <div>
                    <label className={labelClass}>Açıklama</label>
                    <input
                      type="text"
                      placeholder="Nasıl kazanılır?"
                      className={inputClass}
                      onChange={(e) =>
                        setBadgeData({
                          ...badgeData,
                          description: e.target.value,
                        })
                      }
                    />
                  </div>

                  <div>
                    <label className={labelClass}>İlgili Kategori</label>
                    <select
                      className={inputClass}
                      onChange={(e) =>
                        setBadgeData({
                          ...badgeData,
                          categoryId: e.target.value,
                        })
                      }
                      required
                    >
                      <option value="">Seçiniz...</option>
                      {categories.map((c) => (
                        <option key={c.id} value={c.id}>
                          {c.name}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div className="grid grid-cols-2 gap-4">
                    <div>
                      <label className={labelClass}>İkon</label>
                      <input
                        type="text"
                        placeholder="🥇"
                        className={inputClass}
                        onChange={(e) =>
                          setBadgeData({
                            ...badgeData,
                            iconPath: e.target.value,
                          })
                        }
                      />
                    </div>
                    <div>
                      <label className={labelClass}>Gerekli Level</label>
                      <input
                        type="number"
                        placeholder="5"
                        className={inputClass}
                        onChange={(e) =>
                          setBadgeData({
                            ...badgeData,
                            requiredLevel: parseInt(e.target.value),
                          })
                        }
                      />
                    </div>
                  </div>
                </>
              )}

              <button
                disabled={loading}
                type="submit"
                className="w-full bg-emerald-600 hover:bg-emerald-500 text-white py-4 rounded-xl font-bold transition-all shadow-lg shadow-emerald-900/20 active:scale-[0.98] uppercase tracking-widest mt-4"
              >
                {loading ? "İşleniyor..." : "Veritabanına Kaydet"}
              </button>
            </form>
          </div>

          <div className="bg-slate-900/40 p-10 rounded-[3rem] border border-slate-800 flex flex-col relative overflow-hidden">
            <div className="absolute top-0 left-0 w-32 h-32 bg-rose-500/10 blur-[60px] rounded-full"></div>
            <div className="flex justify-between items-center mb-8 border-b border-slate-800 pb-4 relative z-10">
              <h3 className="text-white font-black italic uppercase text-lg tracking-widest">
                Toplu{" "}
                {activeTab === "tasks"
                  ? "Görev"
                  : activeTab === "categories"
                  ? "Kategori"
                  : "Rozet"}{" "}
                Yükle
              </h3>
              <span className="text-[10px] bg-slate-800 text-slate-400 px-2 py-1 rounded border border-slate-700 font-mono">
                JSON Format
              </span>
            </div>

            <textarea
              value={jsonInput}
              onChange={(e) => setJsonInput(e.target.value)}
              className="flex-1 min-h-[400px] bg-slate-950/80 border border-slate-800 rounded-3xl p-6 text-emerald-400 font-mono text-xs outline-none focus:border-rose-500 transition-colors relative z-10 resize-none"
              placeholder={`[\n  { "name": "Örnek Veri", ... }\n]`}
            />

            <button
              disabled={loading}
              onClick={handleBulkSubmit}
              className="mt-6 w-full bg-white hover:bg-slate-200 text-slate-950 font-black py-4 rounded-2xl text-xs uppercase tracking-widest shadow-lg transition-colors relative z-10"
            >
              {loading
                ? "YÜKLENİYOR..."
                : `TOPLU ${activeTab.toUpperCase()} GÖNDER`}
            </button>
          </div>
        </div>
      </div>
    </MainLayout>
  );
};

export default AdminPage;