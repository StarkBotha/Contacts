<script>
export default {
    props: ['contact'],
    methods: {
        onEditClicked(val){
            this.$emit('editclicked', this.contact);
        },
        onDeleteClicked(val){
            this.$emit('deleteclicked', this.contact.id);
        }
    },
}
</script>


<template>
    <li class="list-group-item">{{contact.firstname}} {{contact.lastname}} {{contact.contactNumber}} <a class="link" @click="onEditClicked">Edit</a> <a class="link" @click="onDeleteClicked">Delete</a></li>
</template>