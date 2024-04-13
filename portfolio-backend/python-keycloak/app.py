from keycloak import KeycloakOpenIDConnection, KeycloakAdmin

# Configure client

keycloak_openid = KeycloakOpenIDConnection(
    server_url="http://localhost:8080",
    username="admin1",
    password="admin1",
    realm_name="csharp",
    client_id="poc-client",
    client_secret_key="T50kaB0NKSVmQC5xq394zvP5JKsIh9oM",
)

print(keycloak_openid._token)
