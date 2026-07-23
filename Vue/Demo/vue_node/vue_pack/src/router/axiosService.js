// axiosService.js
import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://api.example.com',
  timeout: 10000,
});

export default {
  get(url) {
    return instance.get(url);
  },
  post(url, data) {
    return instance.post(url, data);
  },
};