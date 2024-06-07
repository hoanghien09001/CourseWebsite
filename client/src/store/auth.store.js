
const user = JSON.parse(localStorage.getItem('user'))

const initialState = user ?
    { status: { loggedIn: true }, user } :
    { status: { loggedIn: false }, user: null };

const auth = {
    namespaced: true,
    state: initialState,
    mutations: {
        loginSuccess(state, user) {
            state.status.loggedIn = true;
            state.user = user;
        },
        loginFailure(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
        logout(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
        

    },
    actions: {
        loginSuccess(context, user) {
            context.commit('loginSuccess', user)
        },
        loginFailure(context) {
            context.commit('loginFailure')
        },
        logout(context) {
            context.commit('logout')
        },
        

    }
}

export default auth;