<template>
    <div>
        <course-card></course-card>
    </div>
</template>

<script>
    import CourseCard from "@/components/CourseCard.vue"

    export default{
  components: { CourseCard },
        comments:{
            "course-card": CourseCard
        },
    }
</script>