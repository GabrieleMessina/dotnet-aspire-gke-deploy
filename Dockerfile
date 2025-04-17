FROM mcr.microsoft.com/dotnet/sdk:9.0

# Install Docker CLI
# RUN apt-get update && \
#     apt-get install -y --no-install-recommends \
#         ca-certificates \
#         curl \
#     install -m 0755 -d /etc/apt/keyrings && \
#     curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc && \
#     chmod a+r /etc/apt/keyrings/docker.asc && \
#     echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] \
#         https://download.docker.com/linux/ubuntu \
#         $(. /etc/os-release && echo \"${UBUNTU_CODENAME:-$VERSION_CODENAME}\") stable" \
#         | tee /etc/apt/sources.list.d/docker.list > dev/null && \
#     apt-get update && \
#     apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin


RUN apt-get update
RUN apt-get install ca-certificates curl 
RUN install -m 0755 -d /etc/apt/keyrings
RUN curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
RUN chmod a+r /etc/apt/keyrings/docker.asc
RUN echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/debian \
        $(. /etc/os-release && echo \"$VERSION_CODENAME\") stable" | \
        tee /etc/apt/sources.list.d/docker.list > /dev/null
RUN apt-get update

RUN apt-get install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

# Verify Docker CLI installation
RUN docker --version
