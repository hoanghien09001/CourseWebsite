<template>
  <nav class="navbar navbar-expand-lg navbar-light p-0">
    <div class="container-fluid">
      <!-- Logo   -->
      <router-link class="navbar-brand ps-2" to="/">
        <img height="45px" src="../assets/images/logos.png" alt="" />
      </router-link>

      <div class="collapse navbar-collapse d-flex">
        <!-- blog view -->
        <router-link :to="{ name: 'BlogPage' }">
          <button class="btn me-2">
            <font-awesome-icon :icon="['fas', 'newspaper']" />
            Blog
          </button>
        </router-link>

        <!-- Thanh tìm kiếm -->
        <form class="d-flex flex-grow-1">
          <input
            class="form-control me-2 py-2"
            type="search"
            placeholder="Tìm kiếm"
          />
          <button class="btn" type="submit">
            <font-awesome-icon
              class="search-icon"
              :icon="['fas', 'magnifying-glass']"
            />
          </button>
        </form>

        <!-- Button đăng ký giảng viên -->
        <div class="ms-3 d-flex teacher-register py-4" v-if="!isTeacher">
          <router-link
            class="navbar-nav d-block me-2 btn-teacher-register"
            :to="{ name: 'TeachingRegisterPage' }"
          >
            Giảng dạy trên Coursery
          </router-link>

          <div class="dropdown-teacher-register">
            <h5>
              Biến kiến thức của bạn thành cơ hội và tiếp cận với hàng triệu
              người trên thế giới.
            </h5>
            <router-link :to="{ name: 'TeachingRegisterPage' }">
              <button class="btn btn-dark">Tìm hiểu thêm</button>
            </router-link>
          </div>
        </div>

        <!-- Giảng viên quản lý khóa học -->
        <router-link
          v-else
          class="btn-teacher"
          :to="{ name: 'CreateCoursePage' }"
          >Giảng viên</router-link
        >

        <!-- giỏ hàng -->
        <button v-if="isLoggedIn" class="btn">
          <router-link :to="{ name: 'ShoppingCart' }">
          <font-awesome-icon class="d-block" :icon="['fas', 'cart-shopping']" />
          </router-link>
        </button>

        <!-- thông báo -->
        <button v-if="isLoggedIn" class="btn">
          <font-awesome-icon :icon="['fas', 'bell']" />
        </button>

        <!-- Đăng ký/đăng nhập -->
        <div class="ms-3" v-if="!isLoggedIn">
          <router-link :to="{ name: 'LoginPage' }">
            <button class="btn btn-outline-secondary login" type="button">
              Đăng nhập
            </button>
          </router-link>
          <router-link :to="{ name: 'RegisterPage' }">
            <button class="btn btn-dark ms-2 register" type="button">
              Đăng ký
            </button>
          </router-link>
        </div>

        <!-- menu người dùng -->
        <div v-if="isLoggedIn" class="avatar menu-dropdown">
          <div class="btn-group dropstart">
            <button
              type="button"
              class="btn"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              <v-avatar color="surface-variant" size="30"
                ><img :src="user.avatar" width="120px" alt=""
              /></v-avatar>
            </button>
            <ul class="dropdown-menu">
              <router-link
                class="d-flex user-profile"
                :to="{ name: 'EditProfile' }"
              >
                <div style="margin: 1rem;">
                  <v-avatar color="surface-variant" size="50"
                    ><img :src="user.avatar" width="120px" alt=""
                  /></v-avatar>
                </div>
                <div class="mt-3 d-flex flex-column justify-content-center">
                  <h5>{{ user.fullname }}</h5>
                  <p>{{ user.email }}</p>
                </div>
              </router-link>
              <hr />

              <router-link :to="{ path: '/cart' }">
                <p>Giỏ hàng của tôi</p>
              </router-link>
              <router-link :to="{ name: 'TeachingRegisterPage' }">
                <p>Giảng dạy trên Coursery</p>
              </router-link>
              <router-link to="/">
                <p>Khóa học của tôi</p>
              </router-link>
              <router-link :to="{ name: 'EditProfile' }">
                <p>Chỉnh sửa hồ sơ</p>
              </router-link>
              <hr />
              <router-link to="/" v-on:click="logout">
                <p>Đăng xuất</p>
              </router-link>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import authServices from "@/services/auth.services";
import { userApi } from "@/services/userApi";
export default {
  data() {
    return {
      userApi: userApi(),
      user: {},
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
    isTeacher() {
      if (this.isLoggedIn) {
        return this.$store.state.auth.user.Permission.includes("Teacher");
      }
      return false;
    },
  },

  async mounted() {
    const userInfo = JSON.parse(localStorage.getItem("user"));
    console.log(userInfo);
    const res = await this.userApi.GetById(userInfo.Id);
    this.user = res.data.data;
    console.log(this.user);
  },
  methods: {
    logout() {
      authServices.logout();
      this.$store.dispatch("auth/logout");
    },
  },
};
</script>

<style scoped>
nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: black;
}

a {
  text-decoration: none;
}

nav {
  box-shadow: 0 3px 8px #888888;
}

input {
  border-radius: 18px;
}

.teacher-register {
  position: relative;
}

button {
  border-radius: 0;
  font-weight: bold;
}

.dropdown-teacher-register {
  display: none;
  position: absolute;
  text-align: center;
  top: 105%;
  font-weight: 900;
  padding: 20px 26px;
  right: 0;
  width: 300px;
  background-color: white;
  z-index: 1;
  box-shadow: 0px 5px 5px #888888;
}

.dropdown-teacher-register button {
  width: 100%;
  font-size: large;
}

.teacher-register:hover .dropdown-teacher-register {
  display: block;
}

.avatar ul {
  width: 260px;
}

.avatar img {
  width: 32px;
}

.avatar ul img {
  width: 64px;
  height: 64px;
  margin: 10px;
}

.avatar ul .user-profile h5 {
  font-size: 16px;
  font-weight: bold;
}

.avatar ul .user-profile p {
  font-size: 12px;
}

.avatar hr {
  margin: 0;
}

.avatar a > p {
  font-weight: lighter;
  font-size: 16px;
  padding: 16px 0 8px 16px;
  margin: 0;
}

.avatar a > p:hover {
  color: #5624d0;
}

.avatar .router-link-exact-active {
  color: #2c3e50;
}
.register {
  color: white;
}
</style>
