<template>
  <div>
    <p>Sign-in in progress</p>
  </div>
</template>

<script>
import mgr from '../services/security';
import {store} from '../store';
export default {
  async created() {
    try {
      var result = await mgr.signinRedirectCallback();
      var returnToUrl = "/";
      if (result.state !== undefined) {
        returnToUrl = result.state;
      }
      store.commit('updateUser');
      //console.log(returnToUrl);
      this.$router.push({ path: returnToUrl });
    } catch (e) {
      this.$router.push({ name: "Unauthorized" });
    }
  }
};
</script>