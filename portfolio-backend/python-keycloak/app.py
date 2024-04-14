from keycloak import KeycloakOpenIDConnection, KeycloakOpenID
from keycloak.exceptions import KeycloakAuthenticationError

# Configure client with KeycloakOpenIdConnection

# keycloak_openid = KeycloakOpenIDConnection(
#     server_url="http://localhost:8080",
#     username="admin1",
#     password="admin1",
#     realm_name="csharp",
#     client_id="poc-client",
#     client_secret_key="T50kaB0NKSVmQC5xq394zvP5JKsIh9oM",
# )

# print(keycloak_openid._token)

# Configure openid with KeycloakOpenID
# the documentation have a bad server_url example, don't use /auth/

keycloak_openid = KeycloakOpenID(
    server_url="http://localhost:8080",
    client_id="poc-client",
    realm_name="csharp",
    client_secret_key="T50kaB0NKSVmQC5xq394zvP5JKsIh9oM",
    verify=True,  # Set to False if you want to skip SSL verification
)


# Obtain an access token
try:
    token = keycloak_openid.token(username="admin", password="admin1")
    # access_token = token["access_token"]
    print("Access Token:", token)
except KeycloakAuthenticationError as e:
    print("Failed to obtain token:", e)
