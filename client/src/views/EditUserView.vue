<template>
  <div class="container-fluid mt-3">
    <div class="row">
      <div class="col-2">
        <div class="avatar">
          <v-avatar color="surface-variant" size="80"
            ><img :src="user.avatar" width="120px" alt=""
          /></v-avatar>
          <p>
            <strong>username</strong>
          </p>
        </div>

        <div class="navbar">
          <router-link :to="{ name: 'EditProfile' }">
            <span>Hồ sơ</span>
          </router-link>

          <router-link :to="{ name: 'ShoppingCart' }">
            <span>Giỏ hàng</span>
          </router-link>

          <router-link :to="'/'">
            <span>Khóa học của bạn</span>
          </router-link>

          <router-link :to="{ name: 'ChangePassword' }">
            <span>Bảo mật tài khoản</span>
          </router-link>

          <router-link :to="'/'">
            <span @click="logout">Đăng xuất</span>
          </router-link>

          <router-link :to="'/'">
            <span>Đóng tài khoản</span>
          </router-link>
        </div>
      </div>

      <div class="col-8">
        <router-view></router-view>
      </div>
    </div>
  </div>
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
  async mounted() {
    const userInfo = JSON.parse(localStorage.getItem("user"));

    const res = await this.userApi.GetById(userInfo.Id);
    this.user = res.data.data;
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
.col-2,
.col-8 {
  border: 1px solid grey;
  text-align: center;
  padding: 0;
}

.col-8 {
  border-left: none;
}

.col-2 a {
  color: rgb(48, 47, 47);
  text-decoration: none;
  font-weight: bold;
  width: 100%;
  margin: 0;
  padding: 8px 0 8px 15px;
  text-align: start;
}

.col-2 a:hover {
  background-color: grey;
  color: white;
}
</style>
