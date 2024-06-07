<template>
  <div class="card" style="width: 232px; height: 280px">
    <img
      :src="theCourse.imageCourse"
      style="width: 130px"
      class="card-img-top"
      alt=""
    />
    <div class="card-body">
      <h5 class="card-title">{{ theCourse.name }}</h5>
      {{ theCourse.creator }}
      <p class="card-text">
        {{ theCourse.introduce }}
      </p>
      {{ "$" + theCourse.price }}
    </div>
  </div>
</template>

<script>
export default {
  props: ["course"],
  data() {
    return {
      thecourse: null,
    };
  },
  computed: {
    theCourse() {
      this.thecourse = this.course;
      return this.thecourse;
    },
  },
};
</script>
