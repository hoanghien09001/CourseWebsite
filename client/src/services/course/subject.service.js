import axios from "axios";
import AuthHeader from "../auth-header.services";
const BaseURL = process.env.VUE_APP_BASE_API_URL;
const auth = localStorage.getItem("accessToken");

class SubjectService {
  getAllSubjectByCourseId(id) {
    return axios
      .get(
        BaseURL + `/Subject/get-all-subject/${id}`,
        {
          headers: {
            Authorization: `Bearer ${auth}`,
          },
        }
      )
      .then((res) => {
        if (res.status == 200) {
          return Promise.resolve(res);
        }
        return Promise.reject(res);
      })
      .catch((err) => {
        return Promise.reject(err);
      });
  }

  createSubjectToCourse(courseid, subject) {
    return axios
      .post(
        BaseURL + "/Subject/add-subject-in-course",
        subject,
        {
          params: {
            courseId: courseid,
          },
        },
        {
          headers: {
            Authorization: `Bearer ${auth}`,
          },
        }
      )
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

  addSubjectDetail(subjectDetail) {
    let form = new FormData();

    form.append("subjectDetailName", subjectDetail.subjectDetailName);
    form.append("linkVideo", subjectDetail.linkVideo);
    form.append("subjectId", subjectDetail.subjectId);

    return axios
      .post(BaseURL + "/Subject/add-subjectdetail", form, {
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

  getAllSubjectDetail(subjectId) {
    return axios
      .get(BaseURL + "/Subject/get-all-subjectdetails/" + subjectId, {
        headers: {
          Authorization: `Bearer ${auth}`,
        },
      })
      .then((res) => {
        if (res.status == 200) {
          return Promise.resolve(res);
        }
        return Promise.reject(res);
      })
      .catch((err) => {
        return Promise.reject(err);
      });
  }
}

export default new SubjectService();
