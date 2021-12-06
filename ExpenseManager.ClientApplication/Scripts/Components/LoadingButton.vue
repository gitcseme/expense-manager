<template>
    <div class="loading-button">
        <button :type="type" :class="[classList, {'disabled':show_loading}]" @click="btnClicked">
            {{text}}
            <img style="height: 22px;" v-if="show_loading" src="../Assets/images/loading-spinner.gif" alt="loading...">
        </button>
    </div>
</template>

<script>
export default {
    props:['text', 'show_loading', 'type', 'classList'],
    methods: {
        btnClicked(){
            this.$emit('btnClieked'); //not use
        }
    }
}
</script>