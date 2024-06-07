<template>
    <div class="container-fluid">
      <form>
        <div class="form-group">
          <label class="kh-form-label" for=""><b>Confirm Code</b></label>
          <input
            class="form-input"
            type="text"
            placeholder="Confirm Code"
            v-model="request.confirmCode"
          />
        </div>
        <div class="form-group">
          <label class="kh-form-label" for=""><b>New Password</b></label>
          <input
            class="form-input"
            type="password"
            placeholder="New Password"
            v-model="request.newPassword"
          />
        </div>
        <div class="form-group">
          <label class="kh-form-label" for=""><b>Confirm Password</b></label>
          <input
            class="form-input"
            type="password"
            placeholder="Confirm Password"
            v-model="request.confirmPassword"
          />
        </div>
        <button
          type="button"
          class="btn btn-primary btn-large form-input"
          @click="confirmNewPassword"
        >
          <b>Xác nhận</b>
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
          confirmCode: "",
          newPassword: "",
          confirmPassword: ""
        },
      };
    },
    methods: {
        async confirmNewPassword(){
            const res = await this.userApi.confirmNewPassword(this.request);
            this.$router.push({path: "/login"});
        }
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