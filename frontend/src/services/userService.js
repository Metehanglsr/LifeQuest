import axiosClient from '../api/axiosClient';

const userService = {
  getProfile: () => axiosClient.get('/users/profile'),
  getLeaderboard: () => axiosClient.get('/users/leaderboard'),
  
  assignRole: (data) => axiosClient.put('/roles/assign-user', data),
};

export default userService;