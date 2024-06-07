import axios from "axios";
import { defineStore } from "pinia";

axios.defaults.baseURL = "http://localhost:5162/api";
const auth = localStorage.getItem("accessToken");
export const courseApi = defineStore("courseApi", {
  actions: {
    GetCourseById(id) {
      return new Promise((resolve, reject) => {
        axios
          .get("/Course/get-course", {
            params: {
              id: id,
            },
          })
          .then((res) => resolve(res))
          .catch((err) => reject(err));
      });
    },
    GetAllSubjectByCourseId(courseId) {
      return new Promise((resolve, reject) => {
        axios
          .get(`/Subject/get-all-subject/${courseId}`)
          .then((res) => resolve(res))
          .catch((err) => reject(err));
      });
    },
    GetAllSubjectDetailBySubjectId(subjectId) {
        return new Promise((resolve, reject) => {
          axios
            .get(`/Subject/get-all-subjectdetails/${subjectId}`)
            .then((res) => resolve(res))
            .catch((err) => reject(err));
        });
      },
  },
});
