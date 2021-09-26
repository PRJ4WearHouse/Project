<template>
  <div>
    <h2>Revive your Apparel</h2>
    <Searchbar />
  </div>
</template>

<script>
  import Searchbar from '@/components/Searchbar'

  export default {
    name: 'Homepage',
    components: {
      Searchbar,
    }
  }
</script>

<style scoped>
  h2 {
  color: green;
  text-align: center;
  font-family: "Caveat";
  }
</style>
