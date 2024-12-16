<template>
    <h1> Login </h1>
    <input type="text" id="username-field" placeholder="Username"><br><br>
    <input type="password" id="password-field" placeholder="Password"><br><br>

    <button id="login-btn">Login</button>
    <br>
    <br>
    <button id="login-with-google-button" @click="handlingGoogleLoginAsync()">Login with google</button>
    
  </template>
  
  <script>
  export default {
    name: 'LoginComponent',
    props: {
      
    },
    methods: {
      handlingGoogleLoginAsync() {
        console.log("helloe ");
        fetch("http://localhost:5238/api/auth/google-login", {
          headers: {
           'Access-Control-Allow-Origin':'*',
          }
        }).then(x => console.log(x));
      }
    }
  }

  // eslint-disable-next-line
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  h3 {
    margin: 40px 0 0;
  }
  ul {
    list-style-type: none;
    padding: 0;
  }
  li {
    display: inline-block;
    margin: 0 10px;
  }
  a {
    color: #42b983;
  }
  </style>
  