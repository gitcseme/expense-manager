<template>
    <div class="alert alert-danger text-center" role="alert">
        {{message}}
    </div>
</template>

<script>
export default {
    props: {
        message: { type:String, default:"" }
    }
}
</script>