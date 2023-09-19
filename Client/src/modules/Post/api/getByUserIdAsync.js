import axios from "axios";
import {GET_BY_USERID_URL} from "../constants/PostApiConstants.js";
import {userManager} from "../../../services/AuthService.js"

export const getByUserIdAsync = async (userId, page, take) => {
    const user = await userManager.getUser();
    
    const response = await axios.get(GET_BY_USERID_URL, {
        params: {
            userId: userId,
            page: page,
            take: take
        },
        headers: {
            Authorization: "Bearer " + user.access_token 
        }
    });

    return response;
}