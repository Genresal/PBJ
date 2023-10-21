import axios from "axios";
import { IDENTITY_USER_URL } from "../../../constants/ApiConstants";

export const getUserAsync = async (email) => {
    try {
        var response = await axios.get(IDENTITY_USER_URL, {
            params: {
                email: email
            }
        })
    
        return response;
    }
    catch (error) {
        console.log(error);
    }
}
