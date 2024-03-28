// Wrapper for HTTP requests with Axios
import axios from 'axios';

const apihelper = axios.create({
   baseURL: process.env.REACT_APP_API,
});

// Add an interceptor for all requests
apihelper.interceptors.request.use(config => {
   // Retrieve the access token from React state or a state management system
   const accessToken = localStorage.getItem('token_vegefoods');

   // Add the access token to the Authorization header
   config.headers.Authorization = `Bearer ${accessToken}`;
   return config;
});

export default apihelper;