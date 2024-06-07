<template>
  <div class="header">
    <h3><strong>Đổi mật khẩu</strong></h3>
  </div>
  <hr />
  <div class="content">
    <form class="d-flex flex-column mb-3">
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Mật khẩu cũ</b></label>
        <input
          class="form-input"
          type="password"
          placeholder="Mật khẩu cũ"
          v-model="request.oldPassword"
        />
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Mật khẩu mới</b></label>
        <input
          class="form-input"
          type="password"
          placeholder="Mật khẩu mới"
          v-model="request.newPassword"
        />
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Xác nhận mật khẩu</b></label>
        <input
          class="form-input"
          type="password"
          placeholder="Xác nhận mật khẩu"
          v-model="request.confirmPassword"
        />
      </div>
      <button
        type="button"
        @click="changePassword"
        class="btn btn-secondary btn-large"
      >
        Lưu
      </button>
    </form>
  </div>
</template>

<script>
import { userApi } from "@/services/userApi";

export default {
  data() {
    return {
      userApi: userApi(),
      request: {
        oldPassword: "",
        newPassword: "",
        confirmPassword: "",
      },
    };
  },
  mounted() {
    console.log(this.request);
  },
  methods: {
    async changePassword() {
      const id = JSON.parse(localStorage.getItem("user"));
      console.log(id);
      const res = await this.userApi.changePassword(id.Id, this.request);
    },
  },
};
</script>

<style>
.form-input {
  height: 2.8rem;
  border: 1px solid #2d2f31;
  border-radius: 0;
  display: block;
  padding: 0 1.6rem;
  width: 50%;
}
.form-group {
  min-width: 18rem;
  max-width: 60rem;
  margin-left: 1rem;
}
.kh-form-label {
  display: flex;
  align-items: center;
  padding-bottom: 0.3rem;
  min-height: 2.8rem;
  margin: 0;
}
.btn-large {
  margin-left: 1rem;
  margin-top: 2rem;
  width: 5rem;
}
</style>