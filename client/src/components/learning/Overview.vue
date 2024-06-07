<template>
    <div class="text-h6 ma-4" v-text="course.courseName">
    </div>
    <div class="d-flex flex-wrap ga-3 ma-4">
        <div>
            <div>
                <span>4,5</span>
                <font-awesome-icon icon="star" />
            </div>
            <div class="text-caption">
                2866 xếp hạng
            </div>
        </div>
        <div>
            <div>
                <span v-text="course.numberOfStudent"></span>
            </div>
            <div class="text-caption">
                Học viên
            </div>
        </div>
        <div>
            <div>
                <span v-text="course.totalCourseDuration+' giờ'"></span>
            </div>
            <div class="text-caption">
                Tổng tiền
            </div>
        </div>
    </div>
</template>
<script>
import {courseApi} from "@/services/courseApi"
export default{
  data(){
    return{
      courseApi: courseApi(),
      course:{}
    }
  },
  async mounted() {
    const courseId = this.$route.params.courseId;
    const res = await this.courseApi.GetCourseById(courseId);
    this.course = res.data.data;
  },
  methods: {
  },
}
</script>