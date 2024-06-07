<template>
  <div class="container">
    <img src="https://s.udemycdn.com/error_page/error-desktop-v1.jpg" alt="" />
    <h2>
      <strong> Chúng tôi không tìm được trang mà bạn đang tìm kiếm </strong>
    </h2>
    <router-link to="/">
      <button class="btn btn-outline-info">Quay trở lại Trang chủ</button>
    </router-link>
  </div>
</template>

<style scoped>
button {
  border-radius: 0;
  font-weight: bold;
}

button:hover {
  color: white;
}
</style>
