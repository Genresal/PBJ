import { useState } from 'react'
import { 
    Box, 
    Modal, 
    Button, 
    Grid, 
    Input, 
    InputAdornment, 
    IconButton, 
    InputLabel,
    FormControl } from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';

export default function EditPasswordModal({onOpen, onClose, onSave}) {
    const [passwordRequest, setPasswordRequest] = useState({
        currentPassword : "",
        newPassword : "",
        confirmPassword: ""
    });
    const [showCurrentPassword, setShowCurrentPassword] = useState(false);
    const [showNewPassword, setShowNewPassword] = useState(false);

    const handleSave = () => {
        onSave({
            currentPassword: passwordRequest.currentPassword,
            newPassword : passwordRequest.newPassword,
            confirmPassword: passwordRequest.confirmPassword
        })

        onClose();
    }

    const handleInputChange = (event) => {
        const { name, value } = event.target;

        setPasswordRequest({
            ...passwordRequest,
            [name] : value
        });
    }

    const handleClickShowCurrentPassword = () => {
        setShowCurrentPassword(prev => !prev);
    }

    const handleClickShowNewPassword = () => {
        setShowNewPassword(prev => !prev);
    }

    const handleConfirmPasswordChange = (event) => {
        if (passwordRequest.newPassword == event.target.value) {
            console.log("Password confirmed")
        }
        else {
            console.log("Password didn't confirm yet")
        }
    }

    return (
        <Modal open={onOpen} onClose={onClose}>
            <Box sx={{ 
                    width: 350, 
                    p: 4, 
                    bgcolor: 'background.paper',
                    borderRadius: '4px',
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    border: '2px solid #000',
                    transform: 'translate(-50%, -50%)',
                }
                }>
                <Grid container direction="column"
                rowSpacing={3} style={{margin: "0 auto"}}>
                    <h2 style={{textAlign: "center"}}>Change Password</h2>
                    <Grid item>
                        <FormControl fullWidth variant="filled">
                            <InputLabel htmlFor="standard-adornment-password">Current Password</InputLabel>
                            <Input
                            type={showCurrentPassword ? "text" : "password"}
                            name="currentPassword"
                            fullWidth
                            onChange={handleInputChange}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton
                                        aria-label="toggle password visibility"
                                        onClick={handleClickShowCurrentPassword}
                                    >
                                        {showCurrentPassword ? <VisibilityOff/> : <Visibility/>}
                                    </IconButton>
                                </InputAdornment>
                            }
                            />
                        </FormControl>
                    </Grid>
                    <Grid item>
                        <FormControl fullWidth>
                            <InputLabel htmlFor="standard-adornment-password">New Password</InputLabel>
                            <Input
                            type={showNewPassword ? "text" : "password"}
                            fullWidth
                            name="newPassword"
                            onChange={handleInputChange}
                            endAdornment={
                                <InputAdornment position="end">
                                    <IconButton
                                        aria-label="toggle password visibility"
                                        onClick={handleClickShowNewPassword}
                                    >
                                        {showNewPassword ? <VisibilityOff/> : <Visibility/>}
                                    </IconButton>
                                </InputAdornment>
                            }
                            />
                        </FormControl>
                    </Grid>
                    <Grid item>
                        <FormControl fullWidth>
                            <InputLabel htmlFor="standard-adornment-password">Confirm Password</InputLabel>
                            <Input
                            type={showNewPassword ? "text" : "password"}
                            name="confirmPassword"
                            fullWidth
                            onChange={(event) => {
                                handleInputChange(event); 
                                handleConfirmPasswordChange(event);
                            }}
                            endAdornment={
                                <InputAdornment position="end">
                                    {showNewPassword ? <VisibilityOff/> : <Visibility/>}
                                </InputAdornment>
                            }
                            />
                        </FormControl>
                    </Grid>
                    <Grid item alignSelf="center">
                        <Button variant="contained" onClick={handleSave}>
                            Save Changes
                        </Button>
                    </Grid>
                </Grid>
            </Box>
        </Modal>
    )
}
