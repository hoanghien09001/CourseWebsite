
const blog = {
    namespaced: true,
    state: {
        latestBlog: {},
    },
    mutations: {
        getNewBlog(state, newBlog) {
            state.latestBlog = newBlog
        },
    },
    actions: {
        getNewBlog(context, newBlog) {
            context.commit('getNewBlog', newBlog)
        },
    }
}

export default blog;