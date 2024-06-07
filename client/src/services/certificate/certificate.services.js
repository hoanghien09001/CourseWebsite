import axios from "axios";
import AuthHeader from "../auth-header.services";
const BaseURL = process.env.VUE_APP_BASE_API_URL;
const auth = localStorage.getItem("accessToken");

class CertificateService {
    getAllCertificateType() {
        return axios.get(BaseURL + '/Certificate/get-all-type')
            .then(res => {
                return Promise.resolve(res)
            })
            .catch(err => {
                return Promise.reject(err)
            })
    }

    sendCertificate(certificate) {

        const form = new FormData();

        form.append("name", certificate.name);
        form.append("description", certificate.description);
        form.append("image", certificate.image)
        form.append("certificateTypeId", certificate.type)
        return axios.post(BaseURL + '/Certificate/add-certificate', form, {
            headers: {
              Authorization: `Bearer ${auth}`,
            },
          })
            .then(res => {
                console.log(res);
                if (res.data.status == 200) {
                    return Promise.resolve(res)
                }
                return Promise.reject(res)
            })
            .catch(err => {
                console.log(err);
                return Promise.reject(err)
            })
    }

    getUserHaveCertificate() {
        return axios.get(BaseURL + '/Certificate/get-user-have-certificate', {
            headers: {
              Authorization: `Bearer ${auth}`,
            },
          })
            .then(res => {
                return Promise.resolve(res)
            }).catch(err => {
                return Promise.reject(err)
            })
    }
}

export default new CertificateService();