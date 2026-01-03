import axiosClient from "../api/axiosClient";

const badgeService = {
  getBadgeGallery: async () => {
    const response = await axiosClient.get("/user-badges/gallery");
    return response.data.badges || [];
  },

  createBadge: async (badgeData) => {
    return await axiosClient.post("/Badges/create-badge", badgeData);
  },

  createBulkBadges: async (badgesArray) => {
    return await axiosClient.post("/Badges/create-bulk-badges", {
      badges: badgesArray,
    });
  },
};

export default badgeService;
