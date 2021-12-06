import AccountAPI from '@scripts/API/AccountAPI';

export default {
    sendInvitation (email) {
        return AccountAPI.sendInvitation(email);
    }
}