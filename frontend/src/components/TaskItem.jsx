import React from "react";

const TaskItem = ({ title, xp, difficulty, isCompleted, onComplete }) => {
  const difficultyColors = {
    Easy: "text-emerald-400 bg-emerald-400/10 border-emerald-400/20",
    Medium: "text-amber-400 bg-amber-400/10 border-amber-400/20",
    Hard: "text-rose-400 bg-rose-400/10 border-rose-400/20",
  };

  const safeDifficulty = difficultyColors[difficulty] ? difficulty : "Medium";

  return (
    <div
      className={`p-4 rounded-2xl border bg-slate-900/40 flex items-center justify-between group transition-all hover:bg-slate-800/60 ${
        isCompleted
          ? "opacity-50 grayscale border-slate-800"
          : "border-slate-800 hover:border-slate-700"
      }`}
    >
      <div className="flex items-center gap-4">
        <div
          className={`w-8 h-8 rounded-full border-2 flex items-center justify-center transition-colors ${
            isCompleted
              ? "bg-emerald-500 border-emerald-500"
              : "border-slate-700 group-hover:border-indigo-500"
          }`}
        >
          {isCompleted && (
            <span className="text-white font-bold text-sm">✓</span>
          )}
        </div>

        <div>
          <h4
            className={`font-bold text-base ${
              isCompleted ? "text-slate-400 line-through" : "text-slate-100"
            }`}
          >
            {title}
          </h4>
          <div className="flex gap-2 mt-1.5">
            <span
              className={`text-[10px] px-2 py-0.5 rounded-full border font-bold uppercase ${difficultyColors[safeDifficulty]}`}
            >
              {difficulty || "Normal"}
            </span>
            <span className="text-[10px] text-indigo-400 font-bold uppercase tracking-wider flex items-center gap-1">
              <span>✨</span> +{xp} XP
            </span>
          </div>
        </div>
      </div>

      <button
        onClick={onComplete}
        disabled={isCompleted}
        className={`px-5 py-2.5 rounded-xl text-xs font-black uppercase tracking-wider transition-all shadow-lg ${
          isCompleted
            ? "bg-slate-800 text-slate-500 cursor-not-allowed shadow-none"
            : "bg-indigo-600 hover:bg-indigo-500 text-white shadow-indigo-500/20 active:scale-95"
        }`}
      >
        {isCompleted ? "Tamamlandı" : "Bitir"}
      </button>
    </div>
  );
};

export default TaskItem;