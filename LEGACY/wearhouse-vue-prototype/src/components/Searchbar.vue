<template>
  <div>
    <input name="searchfield" placeholder="Search" type="text" value=""/>
    <input name="searchbutton" type="submit" value="Go!"/><br>
  </div>
</template>

<script>
  export default {
    name: 'Searchbar',
  }
</script>

<style scoped>
  input {
    font-family: "Lato", sans-serif;
    background-color: #333333;
    color: lightcyan;
    opacity: 0.9;
  }
</style>
