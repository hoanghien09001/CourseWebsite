import { createStore } from 'vuex'
import auth from './auth.store'
import blog from './blog.store'

export default createStore({
  modules: { auth, blog }
})
