# Step 1: Build the application
FROM node:20-alpine AS builder

WORKDIR /app

# Kopiraj package fajlove i instaliraj dependencije
COPY package*.json ./
RUN npm install

# Kopiraj ceo projekat
COPY . .

# Build Next.js aplikaciju
RUN npm run build

# Step 2: Run the application
FROM node:20-alpine AS runner

WORKDIR /app

ENV NODE_ENV=production

# Instaliraj samo production dependencije
COPY package*.json ./
RUN npm install --only=production

# Kopiraj build fajlove iz builder stepa
COPY --from=builder /app/.next .next
COPY --from=builder /app/public public
COPY --from=builder /app/next.config.ts .
COPY --from=builder /app/package.json .

# Ako koristiš src/ folder za pages, Next.js već zna to da pronađe.
# Pokreni aplikaciju
CMD ["npm", "start"]