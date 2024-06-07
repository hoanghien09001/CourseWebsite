<template>
  <div class="container-fluid block my-3">
    <div class="avatar d-flex align-items-center">
      <!-- <img src="comment.avatar" alt="" /> -->
      <img
        width="35px"
        class="d-inline-block"
        src="../../assets/images/user.jpg"
        alt=""
      />
      <strong class="d-inline-block mx-2">Vjppro</strong>
      <span> đã bình luận: </span>
    </div>
    <div class="content p-2">
      <!-- <p>{{ comment.content }}</p> -->
      <!-- <strong>{{ comment.userName }}</strong> -->
      <p>
        mmy text of the printing and typesetting industry. Lorem Ipsum has been
        the industry's standard dummy text ever since the 1500s, when an unknown
        printer took a galley of type and scrambled it to make a type specimen
      </p>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      comment: {},
    };
  },
};
</script>

<style scoped>
div.block {
  border: 1px solid rgb(205, 202, 202);
  padding: 8px 0 0 20px;
  border-radius: 18px;
}
</style>
