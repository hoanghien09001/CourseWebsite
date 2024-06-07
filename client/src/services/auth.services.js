import axios from "axios";

class AuthServices {
    register(Request_register) {
        return axios.post(process.env.VUE_APP_BASE_API_URL + '/auth/register', Request_register)
            .then(res => {
                if (res.data.status == 400) {
                    return Promise.reject(res.data)
                }
                return Promise.resolve(res.data)
            })
            .catch(err => {
                return Promise.reject(err)
            })
    }
    confirmEmail(code) {
        return axios.post(process.env.VUE_APP_BASE_API_URL + '/Auth/ConfirmRegisterAccount', {}, {
            params: { confirmCode: code }
        })
            .then(res => {
                console.log(res.data.includes('thành công'));
                if (!res.data.includes('thành công')) {
                    return Promise.reject(res.data)
                }
                return Promise.resolve(res)
            })
    }
    login(request_login) {
        return axios.post(process.env.VUE_APP_BASE_API_URL + '/auth/login', request_login)
            .then(res => {
                if (res.data.status == 400) {
                    return Promise.reject(res.data)
                } else if (res.data.status == 401) {
                    return Promise.reject(res.data)
                }

                localStorage.setItem("accessToken", res.data.data.accessToken);
                localStorage.setItem("refreshToken", res.data.data.refreshToken);
                let userInfo = this.getDataFromToken(res.data.data.accessToken);

                localStorage.setItem("user", JSON.stringify(userInfo))
                let user = JSON.parse(localStorage.getItem('user'))
                console.log(user);
                return Promise.resolve(res.data)
            })
            .catch(err => {
                return Promise.reject(err)
            })
    }
    updateUser(request_updateUser){
        return axios.put(process.env.VUE_APP_BASE_API_URL + '/auth/updateuser', request_updateUser)
        .then(res=>{
            if (res.data.status == 400) {
                return Promise.reject(res.data)
            } else if (res.data.status == 401) {
                return Promise.reject(res.data)
            }

        })
        .catch(err=>{
            return Promise.reject(err)
        })
    }

    getDataFromToken(token) {
        let parts = token.split('.')
        let header = JSON.parse(atob(parts[0]))
        let payload = JSON.parse(atob(parts[1]))
        let signature = parts[2]
        return payload
    }

    logout() {
        localStorage.removeItem('accessToken')
        localStorage.removeItem('refreshToken')
        localStorage.removeItem('user')
    }
}

export default new AuthServices();