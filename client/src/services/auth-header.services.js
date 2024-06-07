export default function AuthHeader() {
    let accessToken = JSON.parse(localStorage.getItem('accessToken'))
    return accessToken ? { Authorization: 'Bearer ' + jwt } : {};
}