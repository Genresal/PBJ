import axios from "axios";
import { API_USER_FOLLOWER_URL } from "../../../constants/ApiConstants";

export const deleteUserFollowerAsync = async (userEmail, followerEmail) => {
    try {
        const response = await axios.delete(API_USER_FOLLOWER_URL, {
            data : {
                userEmail : userEmail,
                followerEmail : followerEmail
            }
        })

        return response;
    }
    catch (error) {
        console.log(error);
    }
}