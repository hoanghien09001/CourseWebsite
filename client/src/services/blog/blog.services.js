import axios from "axios";
import AuthHeader from "../auth-header.services";
const auth = localStorage.getItem("accessToken");

class BLogServices {
    getAllBlog(numberOfPage, sizeOfPage) {
        return axios.get(process.env.VUE_APP_BASE_API_URL + '/Student/GetAllBlog', {
            params: {
                pageNumber: numberOfPage,
                pageSize: sizeOfPage
            }
        })
            .then(res => {
                console.log(res);
                return Promise.resolve(res.data)
            })
            .catch(err => {
                return Promise.reject(err)
            })
    }
    createBlog(blog) {
        return axios.post(
            process.env.VUE_APP_BASE_API_URL + '/Student/CreateBlog',
            blog, {
                headers: {
                  Authorization: `Bearer ${auth}`,
                },
              })
            .then(res => {
                if (res.data.status == 200) {
                    return Promise.resolve(res)
                }
                return Promise.reject(res)
            }).catch(err => {
                return Promise.reject(err)
            })
    }

    getAllCommentEachBlog() {

    }

}

export default new BLogServices();