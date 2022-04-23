<script setup>


import Contact from './components/Contact.vue'

//let _apiUrl = "https://localhost:7184/api/contact/";
//let _headers = {'Content-type': 'application/json'}


</script>


<script>
export default {
  data() {
    return {
      apiUrl: "https://localhost:7184/api/contact",
      headers: {'Content-type': 'application/json'},


      showAddButton: true,
      showSaveButton: false,
      showEditForm: false,
      showCancelButton: false,

      isEdit: false,

      tempContact: {
        id: 0,
        firstname: "",
        lastname: "",
        contactNumber: ""
      },

      filterText: "",

      count: 0,
      contacts: [],
      filteredContacts: []
    }
  },
  methods: {
    showForm(){
      this.showAddButton = false;
      this.showEditForm = true;
    },
    hideForm(){
      this.showAddButton = true;
      this.showEditForm = false;
    },
    onAddClicked() {
      this.isEdit = false;
      this.showForm();
    },
    onCancelClicked() {
      this.tempContact = {
        id: 0,
        firstname: "",
        lastname: "",
        contactNumber: ""
      };
      this.hideForm();
    },
    onSaveClicked() {
      //alert(this.tempContact.contactNumber.trim() != "");
      if (this.tempContact.firstname.trim() != "" && this.tempContact.lastname.trim() != "" && this.tempContact.contactNumber.trim() != "") {

        if (this.isEdit) {

          let payload = JSON.stringify(this.tempContact);
          console.log(payload);
          const requestOptions = {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: payload
          };

          fetch(this.apiUrl, requestOptions).then((response) => {
            this.fetchContacts();
          }).catch( (error) => {
            console.log(error);
          });

        } else {
          
          let payload = JSON.stringify(this.tempContact);
          console.log(payload);
          const requestOptions = {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: payload
          };

          fetch(this.apiUrl, requestOptions).then((response) => {
            this.fetchContacts();
          }).catch( (error) => {
            console.log(error);
          });

        }

        this.hideForm();
      } else { alert("Fields are blank"); }
    },
    onItemEditClicked(val) {
      //alert(JSON.stringify(val));
      this.isEdit = true;
      this.tempContact = val;
      this.showForm();
    },
    onItemDeleteClicked(id){
      const requestOptions = {
          method: "DELETE",
          headers: { "Content-Type": "application/json" }          
        };
      
      fetch(this.apiUrl + "/" + id, requestOptions).then((response) => {
            this.fetchContacts();
        }).catch( (error) => {
          console.log(error);
        });
    },


    fetchContacts() {
      fetch(this.apiUrl, {headers: this.headers})
      .then(res => res.json()).then((response) => {
        this.contacts = response;
        this.filteredContacts = this.contacts;        
        this.filterText = "";
        //console.log(JSON.stringify(response));
      }).catch( (error) => {
        console.log(error);
      })
    }
  },
  mounted() {
    this.fetchContacts()
  },
  watch: {
    filterText(newValue, oldValue) {
      //console.log(`new: ${newValue}  old: ${oldValue}`);
      if (newValue.trim() == "") {
        this.filteredContacts = this.contacts;         
      } else {
        this.filteredContacts = this.contacts.filter((item) => {
          let fullname = item.firstname + " " + item.lastname;
        return fullname.toLowerCase().includes(newValue.toLowerCase())})
      }
    }
  }
}

async function postData(url = '', data = {}) {
    // Default options are marked with *
    //console.log(data);
    //console.log(JSON.stringify(data));

    const response = await fetch(url, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *client
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    return await response.json(); // parses JSON response into native JavaScript objects
}

</script>

<template>

  <div class="container"> 
    <!-- Add button -->
    <button class="btn btn-primary" v-show="showAddButton" @click="onAddClicked">
      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="22" fill="currentColor" class="bi bi-plus-circle-fill" viewBox="0 3 16 16">
        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8.5 4.5a.5.5 0 0 0-1 0v3h-3a.5.5 0 0 0 0 1h3v3a.5.5 0 0 0 1 0v-3h3a.5.5 0 0 0 0-1h-3v-3z"/>
      </svg> Add
    </button>

    <!-- Edit Form -->
    <div class="editContainer" v-show="showEditForm">
      <!-- First name -->
      <div class="mb-3">
        <label for="txtFirstname" class="form-label">Firstname:</label>
        <input class="form-control" id="txtFirstname" v-model="tempContact.firstname">        
      </div>
      <!-- Last name -->
      <div class="mb-3">
        <label for="txtLastname" class="form-label">Password:</label>
        <input class="form-control" id="txtLastname" v-model="tempContact.lastname">
      </div>
      <!-- Contact Number -->
      <div class="mb-3">
        <label for="txtNumber" class="form-label">Number:</label>
        <input class="form-control" id="txtNumber" v-model="tempContact.contactNumber">
      </div>
      <!-- Save button -->
      <button class="btn btn-primary" @click="onSaveClicked">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="22" fill="currentColor" class="bi bi-file-earmark-check-fill" viewBox="0 3 16 16">
          <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm1.354 4.354-3 3a.5.5 0 0 1-.708 0l-1.5-1.5a.5.5 0 1 1 .708-.708L7.5 9.793l2.646-2.647a.5.5 0 0 1 .708.708z"/>
        </svg> Save
      </button>
      <!-- Cancel button -->
      <button class="btn btn-primary" @click="onCancelClicked">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="22" fill="currentColor" class="bi bi-x-square-fill" viewBox="0 3 16 16">
          <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm3.354 4.646L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 1 1 .708-.708z"/>
        </svg> Cancel
      </button>
    </div>

    <!-- Contacts list -->
    <ul class="list-group">
      <!-- Filter -->
      <li class="list-group-item"><input class="form-control" aria-describedby="filterHelp" placeholder="... type here to filter ..." v-model="filterText"></li>

      <!-- retrieved list of contacts -->
      <Contact v-for="contact in filteredContacts" :contact="contact" @editclicked="onItemEditClicked" @deleteclicked="onItemDeleteClicked"></Contact>
    </ul>
  </div>
  
</template>

<style>

#app {
  max-width: 1280px;
  margin: 0 auto;
  padding: 2rem;

  font-weight: normal;
}

</style>
