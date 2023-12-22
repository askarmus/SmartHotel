# Microservices Booking System with MassTransit and RabbitMQ

## Overview

This project demonstrates the implementation of a microservices-based booking system using .NET Core, MassTransit, and RabbitMQ. The system follows a saga pattern to orchestrate the workflow involving booking creation, payment processing, availability updates, and booking status updates.

## Features

- **Booking Service:**
  - Manages the booking process.
  - Orchestrates the workflow using MassTransit and RabbitMQ.
  - Saga pattern implementation to handle state transitions.

- **Payment Service:**
  - Processes payments asynchronously.
  - Communicates with external payment gateways.
  - Sends payment status updates to the Booking Service.

- **Availability Service:**
  - Manages room availability.
  - Updates availability based on bookings.
  - Sends availability status updates to the Booking Service.

- **Authentication Service:**
  - Manages user authentication.
  - Provides authentication tokens for secured operations.

## Technologies Used

- .NET Core
- ASP.NET Core
- Entity Framework Core
- MassTransit
- RabbitMQ
- Docker
- Kubernetes (optional for container orchestration)

## Setup and Usage

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/microservices-booking-system.git
