import { useEffect, useState } from "react";
import taskService from "../services/taskService";
import { useAuth } from "../context/authContext";
import { toast } from "react-toastify";

const TaskList = () => {
  const [tasks, setTasks] = useState([]);
  const { logout } = useAuth();

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const res = await taskService.getAll();
      setTasks(res.data);
    } catch (err) {
      console.error(err);
      toast.error("Görevler çekilemedi.");
    }
  };

  const handleComplete = async (taskId) => {
    try {
      const response = await taskService.complete(taskId);
      if (response.data.isSuccess) {
        toast.success(`Tebrikler! ${response.data.earnedXP} XP kazandın! 🎉`);
      }
    } catch (err) {
      toast.error(err.response?.data?.message || "Bir hata oluştu");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: "20px",
        }}
      >
        <h1>📌 Görev Listesi</h1>
        <button
          onClick={logout}
          style={{
            background: "red",
            color: "white",
            padding: "8px 15px",
            border: "none",
            cursor: "pointer",
          }}
        >
          Çıkış Yap
        </button>
      </div>

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "repeat(auto-fill, minmax(300px, 1fr))",
          gap: "20px",
        }}
      >
        {tasks.map((task) => (
          <div
            key={task.id}
            style={{
              border: "1px solid #ccc",
              padding: "20px",
              borderRadius: "10px",
              background: "#f9f9f9",
              boxShadow: "0 2px 5px rgba(0,0,0,0.1)",
            }}
          >
            <h3 style={{ marginTop: 0 }}>{task.title}</h3>
            <p style={{ color: "#666" }}>{task.description}</p>
            <div
              style={{
                display: "flex",
                justifyContent: "space-between",
                fontSize: "14px",
                marginBottom: "15px",
              }}
            >
              <span>⚡ {task.baseXP} XP</span>
              <span>🔥 Zorluk: {task.difficulty}</span>
            </div>
            <button
              onClick={() => handleComplete(task.id)}
              style={{
                width: "100%",
                padding: "10px",
                background: "green",
                color: "white",
                border: "none",
                borderRadius: "5px",
                cursor: "pointer",
              }}
            >
              Görevi Tamamla ✅
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default TaskList;