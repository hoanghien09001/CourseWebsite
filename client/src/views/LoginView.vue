<template>
  <div class="login-form">
    <h5>Đăng nhập tài khoản Coursery của bạn</h5>

    <form @submit.prevent="login()">
      <button class="btn btn-outline-dark login-google">
        Tiếp tục với Google
      </button>
      <span class="d-block my-3" style="font-size: medium">hoặc</span>

      <input
        type="text"
        placeholder="Tên đăng nhập"
        v-model="user.userName"
        autocomplete="off"
      />
      <input
        type="password"
        placeholder="Mật khẩu"
        v-model="user.password"
        autocomplete="off"
      />

      <button class="btn btn-dark mb-3" style="background-color: #5624d0">
        Đăng nhập
      </button>
      <span class="d-block">
        <router-link :to="{ name: 'ForgotPassword' }">
          <strong> Quên mật khẩu </strong>
        </router-link>
      </span>
      <hr />
      <router-link :to="{ name: 'RegisterPage' }">
        <p>
          Bạn chưa có tài khoản?
          <strong> Đăng ký ngay </strong>
        </p>
      </router-link>
    </form>
  </div>
</template>

<script>
import authServices from "@/services/auth.services";

export default {
  data() {
    return {
      user: {},
    };
  },
  methods: {
    login() {
      authServices
        .login(this.user)
        .then((res) => {
          this.$toast.success(res.message, {
            duration: 5000,
          });

          let user = JSON.parse(localStorage.getItem("user"));
          this.$store.dispatch("auth/loginSuccess", user);
          let role = user.Permission;
          console.log("log user ne: ", role);
          console.log("Kiểm tra "+role.includes("Admin"));
          if (role.includes("Admin")) {
            console.log("ua alo");
            this.$router.push({ name: "TeacherManagement" });
          } else {
            
            this.$router.push({ name: "HomePage" });
          }
        })
        .catch((err) => {
          this.user = {};
          this.$store.dispatch("auth/loginFailure");
          this.$toast.error(err.message);
        });
    },
  },
};
</script>

<style scoped>
.login-form {
  text-align: center;
  width: 450px;
  padding: 4.8rem 2.4rem;
  margin: 0 auto;
}

button {
  width: 100%;
  border-radius: 0;
  border: 1px solid black;
}

input {
  padding: 10px;
  display: block;
  width: 100%;
  margin: auto;
  margin-bottom: 10px;
}

hr {
  width: 80%;
  margin: 10px auto;
}

a {
  text-decoration: none;
  color: black;
}

strong:hover {
  cursor: pointer;
  color: blueviolet;
}
</style>
