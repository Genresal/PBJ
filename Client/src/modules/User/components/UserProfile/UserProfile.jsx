import { useState } from 'react'
import { editUserAsync } from '../../api/editUserAsync';
import { Grid, Button, Stack, Avatar } from "@mui/material"
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import EditUserModal from '../EditUserModal/EditUserModal'
import { editPasswordAsync } from '../../api/editPasswordAsync';
import EditPasswordModal from '../EditPasswordModal/EditPasswordModal';

export default function UserProfile({loggedUser, saveLoggedUserUser}) {
    const [openEditUserModal, setOpenEditUserModal] = useState(false);
    const [openEditPasswordModal, setOpenEditPasswordModal] = useState(false);

    const handleOpenEditUserModal = () => {
        setOpenEditUserModal(true);
    };

    const handleOpenEditPasswordModal = () => {
        setOpenEditPasswordModal(true);
    }
    
    const handleCloseEditUserModal = () => {
        setOpenEditUserModal(false);
    };

    const handleCloseEditPasswordModal = () => {
        setOpenEditPasswordModal(false);
    }
    
    const handleSaveUser = async (editedUser) => {
        const response = await editUserAsync(loggedUser.email, {
            userName : editedUser.userName,
            surname : editedUser.surname,
            birthDate : new Date(editedUser.birthDate).toISOString()
        });

        if(response) {
            saveLoggedUserUser(editedUser);
        }
    };

    const handleSavePassword = async (passwordRequest) => {
        const response  = await editPasswordAsync(loggedUser.email, passwordRequest)

        if (response) {
            console.log("Password changed successfully");
        }
    }

    return (
        <>
            <Grid container direction="column" style={{border: "1px solid lightgray", padding: "10px 10px"}}>
                <Grid item style={{backgroundColor: "gray", height: 150}}>
                </Grid>
                <Grid columnSpacing={30} container direction="row" justifyContent="space-between"
                    style={{height: 100}}>
                    <Grid item>
                        <Stack>
                            <Avatar
                                src="src/images/testAvatar.jpg"
                                sx={{
                                    width: 150, 
                                    height: 150, 
                                    border: "4px solid white", 
                                    position: "relative",
                                    bottom: 70,
                                    left: 15
                                }}
                            />
                        </Stack>
                    </Grid>
                    <Grid item style={{paddingTop: 15}}>
                        <Button variant="contained" onClick={handleOpenEditUserModal} 
                            style={{borderRadius: 20, marginBottom: 5, width: 160, textTransform: "none"}}>
                            Edit Profile
                        </Button>
                        <Button variant="contained" onClick={handleOpenEditPasswordModal}
                            style={{ display: 'block', borderRadius: 20, width: 160, textTransform: "none" }}>
                            Change Password
                        </Button>
                    </Grid>
                </Grid>
                <Grid container rowSpacing={1} direction="column" style={{paddingLeft: 15, fontSize: 15}}>
                    <Grid item style={{fontWeight: "bold", fontSize: 20}}>
                        {loggedUser.userName} {loggedUser.surname}
                    </Grid>
                    <Grid item style={{color: "gray"}}>
                        {loggedUser.email}
                    </Grid>
                    <Grid item>
                        <CalendarMonthIcon style={{fontSize: 15, marginRight: 5}}/>{loggedUser.birthDate}
                    </Grid>
                </Grid>
            </Grid>

            <EditUserModal
                loggedUser={loggedUser}
                onOpen={openEditUserModal}
                onClose={handleCloseEditUserModal}
                onSave={handleSaveUser}
            />

            <EditPasswordModal
                onOpen={openEditPasswordModal}
                onClose={handleCloseEditPasswordModal}
                onSave={handleSavePassword}
            />
        </>
    )
}
