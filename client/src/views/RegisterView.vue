<template>
  <div class="register-form">
    <h5>Đăng ký và bắt đầu học</h5>

    <form @submit.prevent="register()">
      <button class="btn btn-outline-dark login-google">
        <!-- <Icon icon="fa-brands:google" /> -->
        Tiếp tục với Google
      </button>
      <span class="d-block my-3" style="font-size: medium">hoặc</span>

      <input
        type="text"
        placeholder="Tên đầy đủ"
        v-model="userRegister.fullName"
        autocomplete="off"
      />
      <input
        type="text"
        placeholder="Tên đăng nhập"
        v-model="userRegister.userName"
        autocomplete="off"
      />
      <input
        type="text"
        placeholder="Email"
        v-model="userRegister.email"
        autocomplete="off"
      />
      <input
        type="password"
        placeholder="Mật khẩu"
        v-model="userRegister.password"
        autocomplete="off"
      />

      <button class="btn btn-dark mb-3">Đăng ký</button>
      <h6 class="d-block">
        Bằng việc đăng ký, bạn đồng ý với
        <strong>Điều khoản sử dụng</strong> và
        <strong>Chính sách về quyền riêng tư</strong>
      </h6>
      <hr />
      <p>
        Bạn đã có tài khoản?
        <router-link :to="{ name: 'LoginPage' }">
          <strong> Đăng nhập ngay </strong>
        </router-link>
      </p>
    </form>
  </div>
</template>

<script>
import authServices from "@/services/auth.services";

export default {
  data() {
    return {
      userRegister: {},
    };
  },
  methods: {
    register() {
      authServices
        .register(this.userRegister)
        .then((res) => {
          console.log(res);
          this.$toast.success(res.message, {
            duration: 5000,
          });

          this.$router.push({ name: "ConfirmEmail" });
        })
        .catch((err) => {
          this.userRegister = {};
          this.$toast.error(err.message);
          console.log(err);
        });
    },
  },
};
</script>

<style scoped>
.register-form {
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
