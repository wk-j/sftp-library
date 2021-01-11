## SFTP

```bash
docker-compose -f sftp/docker-compose.yaml up --build
scp README.md demo@localhost:README.md
```