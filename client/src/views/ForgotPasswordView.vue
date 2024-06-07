<template>
  <div class="container-fluid">
    <form>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Quên mật khẩu</b></label>
        <input
          class="form-input"
          type="email"
          placeholder="Email"
          v-model="filer.email"
        />
      </div>
      <button
        type="button"
        class="btn btn-primary btn-large form-input"
        @click="forgotPasswords"
      >
        <b>Đặt lại mật khẩu</b>
      </button>
    </form>
    <div>
      <span>
        hoặc
        <router-link :to="{ name: 'LoginPage' }">
          <strong> Đăng nhập </strong>
        </router-link>
      </span>
    </div>
  </div>
</template>
<script>
import { userApi } from "@/services/userApi";

export default {
  data() {
    return {
      userApi: userApi(),
      filer: {
        email: "",
      },
    };
  },
  methods: {
    async forgotPasswords() {
      const resfg = await this.userApi
        .forgotPassword(this.filer)
        .then((res) => {
          this.$toast.success(res.data);
          this.$router.push({ path: "/confirm-new-password" });
        })
        .catch(err =>{
          this.$toast.error(err.message);
        });
    },
  },
};
</script>
<style scoped>
.container-fluid {
  width: 600px;
  margin: 100px auto;
  text-align: -webkit-center;
}
.form-input {
  height: 2.8rem;
  border: 1px solid #2d2f31;
  border-radius: 0;
  display: block;
  padding: 0 1.6rem;
  width: 70%;
}
.kh-form-label {
  display: flex;
  align-items: center;
  padding-bottom: 0.3rem;
  min-height: 2.8rem;
  margin: 0;
}
</style>