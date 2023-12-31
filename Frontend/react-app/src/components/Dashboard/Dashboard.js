import React, { useContext } from "react";
import AuthContext from "../../contexts/auth-context";
import { useNavigate } from "react-router-dom";
import NavBar from "../NavBar/NavBar.js";
import { Box, Button } from "@mui/material";
import {
  AccountCircle,
  FormatListBulleted,
  ShoppingCart,
  Groups,
  LocalMall,
} from "@mui/icons-material";
import LocationOnIcon from '@mui/icons-material/LocationOn';

const Dashboard = () => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();

  const role = authCtx.role;
  const verification = authCtx.verification;

  const isAdmin = role === "ADMINISTRATOR";
  const isCustomer = role === "CUSTOMER";
  const isSalesman = role === "SALESMAN";
  const isVerified = verification === "ACCEPTED";

  const profileHandler = async (event) => {
    navigate("/profile");
  };
  const verificationHandler = async (event) => {
    navigate("/verification");
  };

  const ordersHandler = async () => {
    navigate("/orders");
  };

  const productHandler = async () => {
    navigate("/product");
  };

  const shopHandler = async () => {
    navigate("/shop");
  };

  const mapHandler = async () => {
    navigate("/map");
  };

  return (
    <>
      <NavBar />
      {isAdmin && (
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
            backgroundColor: "#243b55",
          }}
        >
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "10px 20px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <AccountCircle
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={profileHandler}
            >
              Profile
            </Button>
          </Box>
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "10px 20px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <Groups
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={verificationHandler}
            >
              Verification
            </Button>
          </Box>
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "10px 20px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <FormatListBulleted
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={ordersHandler}
            >
              Orders
            </Button>
          </Box>
        </Box>
      )}

      {isCustomer && (
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
            backgroundColor: "#243b55",
          }}
        >
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "20px 30px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <AccountCircle
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={profileHandler}
            >
              Profile
            </Button>
          </Box>
          <Box sx={{ m: 2, backgroundColor: "#243b55" }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "20px 30px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <FormatListBulleted
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={ordersHandler}
            >
              Orders
            </Button>
          </Box>
          <Box sx={{ m: 2, backgroundColor: "#243b55" }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "20px 30px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <ShoppingCart
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={shopHandler}
            >
              Shop
            </Button>
          </Box>
        </Box>
      )}

      {isSalesman && (
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
            backgroundColor: "#243b55",
          }}
        >
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "30px",
                padding: "20px 30px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <AccountCircle
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={profileHandler}
            >
              Profile
            </Button>
          </Box>
          {isVerified && (
            <>
              <Box sx={{ m: 2, backgroundColor: "#243b55" }}>
                <Button
                  sx={{
                    color: "#141e30",
                    fontSize: "30px",
                    padding: "20px 30px",
                    "&:hover": {
                      backgroundColor: "#03e9f4",
                    },
                  }}
                  variant="contained"
                  startIcon={
                    <LocalMall
                      sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                    />
                  }
                  size="large"
                  color="primary"
                  onClick={productHandler}
                >
                  Products
                </Button>
              </Box>
              <Box sx={{ m: 2, backgroundColor: "#243b55" }}>
                <Button
                  sx={{
                    color: "#141e30",
                    fontSize: "30px",
                    padding: "20px 30px",
                    "&:hover": {
                      backgroundColor: "#03e9f4",
                    },
                  }}
                  variant="contained"
                  startIcon={
                    <FormatListBulleted
                      sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                    />
                  }
                  size="large"
                  color="primary"
                  onClick={ordersHandler}
                >
                  Orders
                </Button>
              </Box>
              <Box sx={{ m: 2, backgroundColor: "#243b55" }}>
                <Button
                  sx={{
                    color: "#141e30",
                    fontSize: "30px",
                    padding: "20px 30px",
                    "&:hover": {
                      backgroundColor: "#03e9f4",
                    },
                  }}
                  variant="contained"
                  startIcon={
                    <LocationOnIcon
                      sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                    />
                  }
                  size="large"
                  color="primary"
                  onClick={mapHandler}
                >
                  Map
                </Button>
              </Box>
            </>
          )}
        </Box>
      )}
    </>
  );
};

export default Dashboard;
