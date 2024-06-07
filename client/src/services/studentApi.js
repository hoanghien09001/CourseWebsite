import axios from "axios";
import { defineStore } from "pinia";

// Định nghĩa baseURL cho axios

axios.defaults.baseURL = "http://localhost:5162/api";
const auth = localStorage.getItem("accessToken");
export const studentApi = defineStore("studentApi", {
  actions: {
    GetAllQuestion(id) {
      return new Promise((resolve, reject) => {
        axios
          .get("/Student/GetAllQuestion/",{
            params: {
              IdsubjectDetail: id,
            },
          })
          .then((res) => resolve(res))
          .catch((err) => reject(err));
      });
    },
    GetAllQuestionByCourseId(id){
      return new Promise((resolve, reject) => {
        axios
         .get(`/Student/GetAllQuestionByCourseId?courseId=${id}`)
         .then((res) => resolve(res))
         .catch((err) => reject(err));
      });
    },
    GetAllAnswer(id) {
      return new Promise((resolve, reject) => {
        axios
          .get(`/Student/GetAllAnswer?QuestionId=${id}&pageNumber=1&pageSize=10`)
          .then((res) => resolve(res))
          .catch((err) => reject(err));
      });
    },
    GetQuestionById(id) {
      return new Promise((resolve, reject) => {
        axios
          .get(`/Student/GetQuestionById?QuestionId=${id}`)
          .then((res) => resolve(res))
          .catch((err) => reject(err));
      });
    },
    CreateMakeQuestion(id, question){
      return new Promise((resolve, reject) => {
        axios
          .post(`/Student/CreateMakeQuestion?SubjectDetailId=${id}&Question=${question}`,{},
          {
            headers: {
              Authorization: `Bearer ${auth}`,
            },
          }
          )
          .then((res) => resolve(res))
          .catch((err) => reject(err));
      });
    }
  },
});