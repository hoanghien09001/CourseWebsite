import axios from "axios";
import { defineStore } from "pinia";

// Định nghĩa baseURL cho axios
axios.defaults.baseURL = "http://localhost:5162/api";
const auth = localStorage.getItem("accessToken");
export const userApi = defineStore("userApi", {
  actions: {
    updateUser(id, params) {
      return new Promise((resolve, reject) => {
        axios
          .put(
            "/Auth/UpdateUser",
            { ...params },
            {
              headers: {
                "Content-Type": "multipart/form-data",
                Authorization: `Bearer ${auth}`,
              },
            }
          )
          .then((res) => resolve(res))
          .catch((error) => reject(error));
      });
    },
    GetById(id) {
      return new Promise((resolve, reject) => {
        axios
          .get("/Auth/GetUserById", {
            params: {
              id: id,
            },
          })
          .then((res) => resolve(res))
          .catch((error) => reject(error));
      });
    },
    changePassword(id, params) {
      return new Promise((resolve, reject) => {
        axios
          .put(
            "/Auth/ChangePassword",
            { id, ...params },
            {
              headers: {
                "Content-Type": "multipart/form-data",
                Authorization: `Bearer ${auth}`,
              },
            }
          )
          .then((res) => resolve(res))
          .catch((error) => reject(error));
      });
    },
    forgotPassword(param) {
      return new Promise((resolve, reject) => {
        axios
          .post(
            "/Auth/ForgotPassword",null,
            {
              params: {
                email: param.email,
              },
            }
            
          )
          .then((res) => resolve(res))
          .catch((error) => reject(error));
      });
    },
    
    confirmNewPassword(params) {
      return new Promise((resolve, reject) => {
        axios
          .put(
            "/Auth/ConfirmCreateNewPassword",
            {
              ...params
            }
          )
          .then((res) => resolve(res))
          .catch((error) => reject(error));
      });
    },
  },
});
