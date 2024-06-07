import axios from "axios";
import AuthHeader from "../auth-header.services";
const BaseURL = process.env.VUE_APP_BASE_API_URL;
const auth = localStorage.getItem("accessToken");

class CourseService {
  getAllCourseATeacher() {
    return axios
      .get(BaseURL + "/Course/get-all-course-by-teacher", {
        headers: {
          Authorization: `Bearer ${auth}`,
        },
      })
      .then((res) => {
        if (res.data.status == 200) {
          return Promise.resolve(res);
        }
        return Promise.reject(res);
      })
      .catch((err) => {
        return Promise.reject(err);
      });
  }

  createCourse(course, id) {
    let form = new FormData();

    form.append("courseName", course.courseName);
    form.append("introduce", course.introduce);
    form.append("imageCourse", course.image);
    form.append("code", course.code);
    form.append("price", course.price);
    form.append("userId", id);

    return axios
      .post(BaseURL + "/Course/add-course", form, {
        headers: {
          Authorization: `Bearer ${auth}`,
        },
      })
      .then((res) => {
        if (res.data.status == 200) {
          return Promise.resolve(res);
        }
        return Promise.reject(res);
      })
      .catch((err) => {
        return Promise.reject(err);
      });
  }
}

export default new CourseService();
