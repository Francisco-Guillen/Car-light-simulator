# Simulating Car Lighting Using a Velleman K8055 Board

## Description
Using the K8055 Velleman board, various lamps were used, such as:
- Lighting (minimum, medium and maximum);
- Signalling (indicators, brakes, reverse gear);
- Auxiliary (interior, licence plate light);
  
Controlled by various means, such as
- Switches actuated by the driver (low and high beam lever);
- Brake pedal;
- Sensores automáticos de luminosidade.

## K8055 Velleman Board
- The K8055 board has 5 digital input channels and 8 digital output channels. In addition, there are two analogue inputs and two analogue outputs with 8-bit resolution;
- It is possible to increase the number of inputs and outputs by connecting more boards (up to a maximum of four) to the USB connectors on the machine in use;
- All the communication routines are grouped together in a DLL (Dynamic Link Library);

<p align="center">
  <img src="https://github.com/Francisco-Guillen/Car-light-simulator/assets/83434031/32d198a0-e540-4e34-9514-5ccd013a552a">
  <br>
  Figure 1: K8055 Velleman Board
</p>

## Board Architecture
In order to represent the car's automatic sensors and using digital inputs to represent certain scenarios, they were specified as follows: 
- Digital input 1: Brightness sensor -> switches on the high beams and number plate in the event of low light;
- Digital input 2: Fog sensor -> switches on the fog lights automatically in an environment with reduced visibility;
- Digital input 3: Open door sensor -> switches on the car's interior light if a door is found to be open;

To represent the output of a car's lighting control, the output channels were specified as follows: 

<p align="center">
<img src="https://github.com/Francisco-Guillen/Car-light-simulator/assets/83434031/0bf7e264-2257-49ca-8a84-c0ddf1f80165">
">
  <br>
  Figure 2: Output
</p>

## Interface
In order to represent the various levers corresponding to each light, as well as the choice of different lighting scenarios, the following interface was created:
<p align="center">
<img src="https://github.com/Francisco-Guillen/Car-light-simulator/assets/83434031/4daf44c9-58dd-4652-9001-c77f71c17449">
>
<br>
  Figure 3: Interface
</p>
