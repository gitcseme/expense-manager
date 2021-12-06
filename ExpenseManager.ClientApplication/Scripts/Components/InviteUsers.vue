<template>
    <div>
        <div class="form-group">
            <label for="email">Email</label>
            <input 
                type="email" placeholder="Invitation email" 
                class="form-control" style="width: 350px"
                data-vv-name="email" data-vv-as="Email"
                v-validate="'required|email'" 
                v-model="email"
            >
        </div>
        <template v-if="invitationSuccess">
            <div style="width: 350px; background-color: green; padding: 10px 5px; border-radius: 4px;">
                <p style="color: white; font-weight: 800; text-align: center; margin: 0;">Invitation sent</p>
            </div>
        </template>
        <div>
            <button type="button" class="btn btn-primary" @click="inviteUser">Invite</button>
        </div>
    </div>
</template>

<script>
import AccountService from '@scripts/services/AccountService';

export default {
    data() {
        return {
            email: '',
            invitationSuccess: false,
        }
    },
    mounted() {
        console.log(this);
    },
    methods: {
        inviteUser() {
            this.$validator.validateAll().then(result => {
                if (result) {
                    AccountService.sendInvitation(this.email)
                    .then(() => {
                        console.log('User invited');
                        this.invitationSuccess = true;
                        setTimeout(() => {
                            this.invitationSuccess = false;
                        }, 3000);
                    })
                    .catch(error => {
                        console.log(error.message);
                    });
                }
            });
        }
    }
}
</script>