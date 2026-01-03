import React from 'react';
import { useNavigate } from 'react-router-dom';

const CategoryCard = ({ name, iconPath, taskCount }) => {
  const navigate = useNavigate();

  const handleCategoryClick = () => {
    navigate(`/tasks?category=${name}`);
  };

  return (
    <div 
      onClick={handleCategoryClick}
      className="relative group cursor-pointer transition-all duration-300 hover:-translate-y-2"
    >
      <div className="absolute -inset-0.5 bg-gradient-to-r from-indigo-500 to-cyan-500 rounded-[2rem] blur opacity-10 group-hover:opacity-40 transition duration-500"></div>
      <div className="relative p-8 bg-slate-900 rounded-[2rem] border border-slate-800 flex flex-col items-center text-center gap-4 group-hover:border-indigo-500/50 transition-colors">
        <div className="w-16 h-16 bg-slate-800/50 rounded-2xl flex items-center justify-center text-3xl group-hover:scale-110 transition-transform duration-500 shadow-inner">
          {iconPath || '📁'}
        </div>
        <div>
          <h3 className="font-black text-white tracking-tight uppercase italic group-hover:text-indigo-400 transition-colors">{name}</h3>
          <p className="text-[10px] text-slate-500 font-bold uppercase tracking-widest mt-1">{taskCount} Aktif Görev</p>
        </div>
      </div>
    </div>
  );
};

export default CategoryCard;