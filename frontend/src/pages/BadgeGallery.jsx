import React, { useState, useEffect } from "react";
import MainLayout from "../layouts/MainLayout";
import BadgeCard from "../components/BadgeCard";
import badgeService from "../services/badgeService";

const BadgeGallery = () => {
  const [badges, setBadges] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchBadges = async () => {
      try {
        const data = await badgeService.getBadgeGallery();
        setBadges(data);
      } catch (err) {
        console.error("Rozetler yüklenirken hata:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchBadges();
  }, []);

  return (
    <MainLayout>
      <div className="space-y-10">
        <section>
          <h2 className="text-4xl font-black text-white italic tracking-tighter mb-2">
            ROZETLER
          </h2>
          <div className="h-1.5 w-24 bg-indigo-600 rounded-full shadow-[0_0_20px_rgba(79,70,229,0.5)]"></div>
          <p className="text-slate-500 mt-4 font-medium italic">
            Başarılarını sergile ve yeni hedeflere odaklan.
          </p>
        </section>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {!loading ? (
            badges.length > 0 ? (
              badges.map((badge) => <BadgeCard key={badge.id} {...badge} />)
            ) : (
              <p className="text-slate-500 italic col-span-4 text-center">
                Henüz rozet tanımlanmamış.
              </p>
            )
          ) : (
            [1, 2, 3, 4].map((i) => (
              <div
                key={i}
                className="h-64 bg-slate-900/50 rounded-3xl animate-pulse border border-slate-800"
              ></div>
            ))
          )}
        </div>
      </div>
    </MainLayout>
  );
};

export default BadgeGallery;